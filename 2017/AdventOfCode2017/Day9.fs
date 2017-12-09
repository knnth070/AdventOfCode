module Day9

    open System.IO
    open System

    type Character = Group | Garbage | Cancelled

    type State = { level: int; character: Character; garbage: int; score: int }

    let processStream state next =
        match state.character, next with
        | Group, '{' -> { state with level = state.level + 1 }
        | Group, '}' -> { state with score = state.score + state.level; level = state.level - 1 }
        | Group, '<' -> { state with character = Garbage }
        | Garbage, '>' -> { state with character = Group }
        | Garbage, '!' -> { state with character = Cancelled }
        | Garbage, _ -> { state with garbage = state.garbage + 1 }
        | Cancelled, _ -> { state with character = Garbage }
        | _ -> state

    let solve() =
        let input = File.ReadAllLines("..\..\Input\day9.txt").[0]
        
        let result = input |> Seq.fold processStream { level = 0; character = Group; garbage = 0; score = 0 }
        printfn "part 1 = %A" <| result.score
        printfn "part 2 = %A" <| result.garbage
