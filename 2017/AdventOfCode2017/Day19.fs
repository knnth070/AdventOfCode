module Day19

    open System
    open System.IO

    type Direction = Left | Right | Up | Down

    type State =
        {
            direction: Direction;
            X: int;
            Y: int;
            collected: string;
            steps: int;
        }

    let opposite = function
        | Left -> Right
        | Right -> Left
        | Up -> Down
        | Down -> Up

    let isValidPos = (<>) ' '

    let isLine = function
        | '-' | '|' | '+' -> true
        | _ -> false

    let rec route (diagram: string[]) state =
        let charAtPos d =
            let x, y =
                match d with
                | Up -> state.X, state.Y - 1
                | Down -> state.X, state.Y + 1
                | Left -> state.X - 1, state.Y
                | Right -> state.X + 1, state.Y
            if x < 0 || y < 0 || x > diagram.[0].Length - 1 || y > diagram.Length - 1
            then None
            else Some (diagram.[y].[x], d)

        let findDirection =
            match charAtPos state.direction with
            | Some (c, _) when isValidPos c ->
                Some (c, state.direction)
            | _ ->
                let options =
                    [ Left; Right; Up; Down]
                    |> List.filter ((<>) (opposite state.direction))
                    |> List.choose charAtPos
                    |> List.filter (fst >> isValidPos)
                if List.isEmpty options
                then None
                else options |> List.head |> Some

        match findDirection with
        | Some (c, newDirection) ->
            let collected =
                if isLine c
                then state.collected
                else sprintf "%s%c" state.collected c

            let x, y =
                match newDirection with
                | Up -> state.X, state.Y - 1
                | Down -> state.X, state.Y + 1
                | Left -> state.X - 1, state.Y
                | Right -> state.X + 1, state.Y
            route diagram { state with direction = newDirection; X = x; Y = y; collected = collected; steps = state.steps + 1 }
        | None ->
            state.collected, state.steps + 1

    let solve() =
        let input = File.ReadAllLines("..\..\Input\day19.txt")
        let start = { direction = Down; X = 27; Y = 0; collected = ""; steps = 0 }
        let part1, part2 = route input start
        printfn "part 1 = %A" <| part1
        printfn "part 2 = %A" <| part2
