module Day10

    open System.IO
    open System

    let solve() =
        let input = File.ReadAllLines("..\..\Input\day10.txt").[0]
        
        let part1 =
            input
            |> Knothash.hash
            |> List.take 2
            |> List.fold (*) 1
        
        let part2 = Knothash.densehash input

        printfn "%d" part1
        printfn "%s" part2
