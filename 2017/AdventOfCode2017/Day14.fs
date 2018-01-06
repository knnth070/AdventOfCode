module Day14

    let row input number =
        let toSquares = function
            | '0' -> "...."
            | '1' -> "...#"
            | '2' -> "..#."
            | '3' -> "..##"
            | '4' -> ".#.."
            | '5' -> ".#.#"
            | '6' -> ".##."
            | '7' -> ".###"
            | '8' -> "#..."
            | '9' -> "#..#"
            | 'a' -> "#.#."
            | 'b' -> "#.##"
            | 'c' -> "##.."
            | 'd' -> "##.#"
            | 'e' -> "###."
            | 'f' -> "####"
            | _ -> ""

        sprintf "%s-%d" input number
        |> Knothash.densehash
        |> Seq.map toSquares
        |> Seq.concat
        |> Array.ofSeq

    let squareInUse = function
        | '#' -> true
        | _ -> false

    let totalSquaresInUse =
        let squaresInUsePerRow =
            Seq.sumBy (fun c -> if squareInUse c then 1 else 0)

        Seq.sumBy squaresInUsePerRow

    let findAdjacentSquares (x, y) (grid: char [] []) =
        let inUse (x, y) =
            if x < 0 || x > 127 || y < 0 || y > 127
            then None
            else
                if squareInUse grid.[y].[x]
                then Some (x, y)
                else None

        let rec impl toVisit visited =
            match toVisit with
            | [] ->
                visited
            | c::cs when (List.contains c visited) ->
                impl cs visited
            | (x, y)::cs ->
                let squares =
                    [ x - 1, y; x + 1, y; x, y - 1; x, y + 1 ]
                    |> List.choose inUse
                impl (cs @ squares) ((x, y)::visited)

        impl [x, y] []

    let regions (grid: char [] []) =
        let isSquareVisited c =
            List.exists (List.contains c)

        let rec impl (x, y) visited =
            match x, y with
            | 128, 127 ->
                visited
            | 128, _ ->
                impl (0, (y + 1)) visited
            | _ ->
                let newVisited =
                    if squareInUse grid.[y].[x] && not <| isSquareVisited (x, y) visited
                    then
                        let adjacent = findAdjacentSquares (x, y) grid
                        adjacent::visited
                    else visited
                impl ((x + 1), y) newVisited

        impl (0, 0) []

    let solve() =
        let input = "hwlqcszp"
        let grid = [0..127] |> List.map (row input) |> Array.ofList

        printfn "part 1 = %A" <| totalSquaresInUse grid
        printfn "part 2 = %A" <| List.length (regions grid)
