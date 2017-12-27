module Day13

    open System
    open System.IO

    type Layer = { depth: int; range: int; steps: int }

    let parseLine (line:string) =
        let tokens = line.Split(':') |> Array.map int
        { depth = tokens.[0]; range = tokens.[1]; steps = (tokens.[1] - 1) * 2 }

    let notCaught delay layer = 
        (layer.depth + delay) % layer.steps <> 0

    let severity layer =
        if notCaught 0 layer
        then 0
        else layer.depth * layer.range

    let bruteforce scanners =
        let rec impl delay =
            if scanners |> Seq.forall (notCaught delay)
            then delay
            else impl (delay + 1)
        impl 0

    let solve() =
        let input =
            File.ReadAllLines("..\..\Input\day13.txt")
            |> Seq.map parseLine

        printfn "part 1 = %A" <| Seq.sumBy severity input
        printfn "part 2 = %A" <| bruteforce input
