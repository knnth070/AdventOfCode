module Day17

    let insert step (start, l) n =
        let len = List.length l
        let pos = (start + step) % len + 1
        let first = l |> List.take pos
        let tail = l |> List.skip pos

        pos, first @ (n::tail)

    let insertAtOne step (pos, len, numberAtOne) n =
        let newPos = (pos + step) % len + 1

        let newNumberAtOne =
            if newPos = 1
            then n
            else numberAtOne
        newPos, len + 1, newNumberAtOne
    
    let solve() =
        let step = 363

        let pos, result = [1 .. 2017] |> List.fold (insert step) (0, [0])
        let part1 = result |> List.skip (pos + 1) |> List.head

        let _, _, part2 = [1 .. 50_000_000] |> List.fold (insertAtOne step) (0, 1, 0)

        printfn "part 1 = %A" <| part1
        printfn "part 2 = %A" <| part2
