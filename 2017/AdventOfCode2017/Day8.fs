module Day8

    open System.IO
    open System
    open System.Collections.Generic

    type Condition =
        { register: string; comparison: string; value: int }

    let registers = new Dictionary<string, int>()

    let setRegister name value =
        registers.[name] <- value

    let getRegister name =
        match registers.TryGetValue(name) with
        | true, n -> n
        | _ -> 0

    let parseLine (line:string) =
        let tokens = line.Split(' ')
        let reg = tokens.[0]
        let value =
            match tokens.[1] with
            | "inc" -> int tokens.[2]
            | "dec" -> - int tokens.[2]
            | _ -> 0
        let condition = { register = tokens.[4]; comparison = tokens.[5]; value = int tokens.[6] }

        reg, value, condition

    let conditionIsTrue condition =
        let reg = getRegister condition.register
        match condition.comparison with
        | ">" -> reg > condition.value
        | "<" -> reg < condition.value
        | ">=" -> reg >= condition.value
        | "<=" -> reg <= condition.value
        | "==" -> reg = condition.value
        | "!=" -> reg <> condition.value
        | _ -> false

    let processLine (reg, value, condition) =
        if conditionIsTrue condition
        then setRegister reg (value + getRegister reg)

    let mutable highestIntermediate = new KeyValuePair<string,int>()

    let storeHighest() =
        let highest = registers |> Seq.maxBy (fun kv -> kv.Value)
        if highest.Value > highestIntermediate.Value
        then highestIntermediate <- highest

    let solve() =
        File.ReadAllLines("..\..\Input\day8.txt")
        |> Seq.iter (parseLine >> processLine >> storeHighest)
        let highest = registers |> Seq.maxBy (fun kv -> kv.Value)
        printfn "part 1 = %A" highest
        printfn "part 2 = %A" highestIntermediate
