module Day15

    let rec generator seed multiplier divisor =
        let value = (seed * multiplier) % 2147483647UL
        if value % divisor = 0UL
        then
            seq {
                yield value
                yield! generator value multiplier divisor
            }
        else
            generator value multiplier divisor

    let equals16 a b =
        (int16 a) = (int16 b)

    let duel a b divA divB pairs =
        let genA = generator a 16807UL divA
        let genB = generator b 48271UL divB

        Seq.zip genA genB
        |> Seq.take pairs
        |> Seq.filter (fun (a, b) -> equals16 a b)
        |> Seq.length

    let solve() =
        let a, b = 679UL, 771UL

        printfn "part 1 = %A" <| duel a b 1UL 1UL 40_000_000
        printfn "part 2 = %A" <| duel a b 4UL 8UL 5_000_000
