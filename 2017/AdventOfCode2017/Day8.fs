module Day8

    open System.IO
    open System
    open System.Collections.Generic

    type Condition =
        { operand: string; comparison: string; value: int }

    type Instruction =
        { register: string; increment: int; condition: Condition }

    let parseLine (line:string) =
        let tokens = line.Split(' ')
        let reg = tokens.[0]
        let increment =
            match tokens.[1] with
            | "inc" -> int tokens.[2]
            | "dec" -> - int tokens.[2]
            | _ -> 0
        let condition = { operand = tokens.[4]; comparison = tokens.[5]; value = int tokens.[6] }

        { register = reg; increment = increment; condition = condition }

    let highestValue =
        Map.toList >> List.map snd >> List.max

    let processLine registers instruction =
        let setRegister name value =
            registers |> Map.add name value

        let getRegister name =
            registers |> Map.tryFind name |> Option.defaultValue 0

        let conditionIsTrue condition =
            let registerValue = getRegister condition.operand
            match condition.comparison with
            | ">" -> registerValue > condition.value
            | "<" -> registerValue < condition.value
            | ">=" -> registerValue >= condition.value
            | "<=" -> registerValue <= condition.value
            | "==" -> registerValue = condition.value
            | "!=" -> registerValue <> condition.value
            | _ -> false

        if conditionIsTrue instruction.condition
        then
            let newValue = instruction.increment + getRegister instruction.register
            setRegister instruction.register newValue
        else
            registers

    let solve() =
        let result =
            File.ReadAllLines("..\..\Input\day8.txt")
            |> Seq.map parseLine
            |> Seq.scan processLine Map.empty
            |> Seq.skip 1 // skip empty Map
            |> Seq.map highestValue

        printfn "part 1 = %d" <| Seq.last result
        printfn "part 2 = %d" <| Seq.max result
