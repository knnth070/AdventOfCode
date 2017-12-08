module Day8

    open System.IO
    open System
    open System.Collections.Generic

    type Condition =
        { operand: string; comparison: string; value: int }

    type Instruction =
        { register: string; increment: int; condition: Condition }

    let setRegister registers name value =
        registers |> Map.add name value

    let getRegister registers name =
        registers |> Map.tryFind name |> Option.defaultValue 0

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

    let conditionIsTrue registers condition =
        let registerValue = getRegister registers condition.operand
        match condition.comparison with
        | ">" -> registerValue > condition.value
        | "<" -> registerValue < condition.value
        | ">=" -> registerValue >= condition.value
        | "<=" -> registerValue <= condition.value
        | "==" -> registerValue = condition.value
        | "!=" -> registerValue <> condition.value
        | _ -> false

    let highestValue =
        Map.toList >> List.map snd >> List.max

    let processLine registers instruction highest =
        if conditionIsTrue registers instruction.condition
        then
            let newValue = instruction.increment + getRegister registers instruction.register
            let newRegisters = setRegister registers instruction.register newValue
            let currentHighest = highestValue newRegisters
            newRegisters, currentHighest, max highest currentHighest
        else
            registers, highestValue registers, highest

    let solve() =
        let _, highest, totalHighest =
            File.ReadAllLines("..\..\Input\day8.txt")
            |> Seq.map parseLine
            |> Seq.fold (fun (reg, _, h) i -> processLine reg i h) (Map.empty, 0, 0)

        printfn "part 1 = %d" highest
        printfn "part 2 = %d" totalHighest
