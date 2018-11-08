module Day22

    open System.IO
    open System

    type Direction = Up | Down | Left | Right
    type NodeState = Clean | Weakened | Infected | Flagged

    type State = { grid: Map<int*int, NodeState>; position: int*int; direction: Direction; infections: int }

    let parseFile filename =
        let parseLine i (line:string) =
            line.ToCharArray() |> Array.mapi (fun j c -> (j, i), (if c = '#' then Infected else Clean))

        File.ReadAllLines(filename)
            |> Array.mapi parseLine
            |> Array.concat
            |> Map.ofArray

    let turnLeft = function
        | Up -> Left
        | Left -> Down
        | Down -> Right
        | Right -> Up

    let turnRight = turnLeft >> turnLeft >> turnLeft

    let reverse = turnLeft >> turnLeft

    let transitionNodePart1 = function
        | Clean -> Infected
        | Infected -> Clean
        | _ -> failwith "invalid state for part 1"

    let transitionNodePart2 = function
        | Clean -> Weakened
        | Weakened -> Infected
        | Infected -> Flagged
        | Flagged -> Clean

    let burst transition state _ =
        let currentNodeState =
            match Map.tryFind state.position state.grid with
            | Some n -> n
            | None -> Clean

        let newNodeState = transition currentNodeState

        let newDirection =
            (match currentNodeState with
            | Infected -> turnRight
            | Clean -> turnLeft
            | Weakened -> id
            | Flagged -> reverse) state.direction

        let newGrid = state.grid |> Map.add state.position newNodeState

        let newPosition =
            let x, y = state.position
            match newDirection with
            | Up -> (x, y - 1)
            | Down -> (x, y + 1)
            | Left -> (x - 1, y)
            | Right -> (x + 1, y)

        {
            grid = newGrid;
            position = newPosition;
            direction = newDirection;
            infections = state.infections + (if newNodeState = Infected then 1 else 0)
        }

    let solve() =
        let grid = parseFile "..\..\Input\day22.txt"

        let start = { grid = grid; position = (12,12); direction = Up; infections = 0 }

        let burstPart1 = burst transitionNodePart1
        let burstPart2 = burst transitionNodePart2

        let part1 = List.fold burstPart1 start [1..10_000]
        let part2 = List.fold burstPart2 start [1..10_000_000]

        printfn "part 1 = %A" <| part1.infections
        printfn "part 2 = %A" <| part2.infections
