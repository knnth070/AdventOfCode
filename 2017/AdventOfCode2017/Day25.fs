module Day25

open System
open System.IO

type State = A | B | C | D | E | F

type Direction = Left | Right

type Machine = { Tape: Set<int>; Position: int; State: State }

type Instruction = { CurrentState: State; CurrentValue: int; Move: Direction; NewState: State; NewValue: int }

let instructions = [
    { CurrentState = A; CurrentValue = 0; Move = Right; NewState = B; NewValue = 1 }
    { CurrentState = A; CurrentValue = 1; Move = Left; NewState = C; NewValue = 0 }

    { CurrentState = B; CurrentValue = 0; Move = Left; NewState = A; NewValue = 1 }
    { CurrentState = B; CurrentValue = 1; Move = Left; NewState = D; NewValue = 1 }

    { CurrentState = C; CurrentValue = 0; Move = Right; NewState = D; NewValue = 1 }
    { CurrentState = C; CurrentValue = 1; Move = Right; NewState = C; NewValue = 0 }

    { CurrentState = D; CurrentValue = 0; Move = Left; NewState = B; NewValue = 0 }
    { CurrentState = D; CurrentValue = 1; Move = Right; NewState = E; NewValue = 0 }

    { CurrentState = E; CurrentValue = 0; Move = Right; NewState = C; NewValue = 1 }
    { CurrentState = E; CurrentValue = 1; Move = Left; NewState = F; NewValue = 1 }

    { CurrentState = F; CurrentValue = 0; Move = Left; NewState = E; NewValue = 1 }
    { CurrentState = F; CurrentValue = 1; Move = Right; NewState = A; NewValue = 1 }
]

let current state =
    if state.Tape |> Set.contains state.Position
    then 1
    else 0

let step state _ =
    let instruction =
        instructions
        |> List.filter (fun i -> i.CurrentState = state.State && i.CurrentValue = (current state))
        |> List.exactlyOne
    
    let newTape =
        if instruction.NewValue = 1
        then state.Tape |> Set.add state.Position
        else state.Tape |> Set.remove state.Position
    
    let newPosition = state.Position + (if instruction.Move = Left then -1 else 1)

    { Tape = newTape; Position = newPosition; State = instruction.NewState }

let solve() =

    let start = { Tape = Set.empty; Position = 0; State = A }

    let result = [1..12172063] |> List.fold step start
    
    printfn "part 1 = %d" <| Set.count result.Tape
