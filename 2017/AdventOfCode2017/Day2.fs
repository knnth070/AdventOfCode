module Day2

    open System.IO
    open System

    let parseInt s =
        match Int32.TryParse(s) with
        | true, i -> Some i
        | _ -> None
    
    let parseLine (line:string) =
            line.Split([| '\t' |], StringSplitOptions.RemoveEmptyEntries)
            |> Seq.choose parseInt

    let sumChecksum values =
        (Seq.max values) - (Seq.min values)

    let divChecksum values =
        seq {
            for x in values do
                for y in values do
                    yield if x > y && x % y = 0 then Some (x / y) else None
        }
        |> Seq.choose id
        |> Seq.head

    let checksum f input =
        input
        |> Seq.map (parseLine >> f)
        |> Seq.sum

    let solve() =
        let input = File.ReadAllLines("..\..\Input\day2.txt")
        printfn "part 1 = %A" (checksum sumChecksum input)
        printfn "part 2 = %A" (checksum divChecksum input)
        ()
