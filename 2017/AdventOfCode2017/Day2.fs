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
        Seq.map (fun x -> Seq.map (fun y -> x, y) values) values
        |> Seq.concat
        |> Seq.choose (fun (x, y) -> if x > y && x % y = 0 then Some (x / y) else None)
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
