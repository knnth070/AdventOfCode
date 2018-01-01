module Day17

    let insert n step start l =
        let len = List.length l
        let pos = (start + step) % len + 1
        let first = l |> List.take pos
        let tail = l |> List.skip pos

        pos, first @ (n::tail)

    let insertAtOne n step pos len numberAtOne =
        let newPos = (pos + step) % len + 1

        let newNumberAtOne =
            if newPos = 1
            then n
            else numberAtOne
        newPos, len + 1, newNumberAtOne
    
    let solve() =
        let step = 363

        let pos, result = [1 .. 2017] |> List.fold (fun (pos, buffer) t -> insert t step pos buffer) (0, [0])
        let part1 = result |> List.skip (pos + 1) |> List.head

        let _, _, part2 = [1 .. 50_000_000] |> List.fold (fun (pos, len, n) t -> insertAtOne t step pos len n) (0, 1, 0)

        printfn "part 1 = %A" <| part1
        printfn "part 2 = %A" <| part2
