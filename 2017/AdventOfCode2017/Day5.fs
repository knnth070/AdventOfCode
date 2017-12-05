module Day5

    open System.IO
    open System

    let inc = (+) 1
    let incOrDec n = if n >= 3 then n - 1 else n + 1

    let jump f (maze:int[]) =
        let rec impl ip acc =
            if ip >= maze.Length
            then acc
            else
                let jump = maze.[ip]
                maze.[ip] <- f jump
                impl (ip + jump) (acc + 1)
        impl 0 0

    let solve() =
        let input1 = File.ReadAllLines("..\..\Input\day5.txt") |> Array.map int
        let input2 = Array.copy input1
        printfn "part 1 = %A" (jump inc input1)
        printfn "part 2 = %A" (jump incOrDec input2)
        ()
