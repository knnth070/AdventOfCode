module Day23

    open System.IO
    open System
    open Assembly

    let rec run (program:Instruction[]) zero =
        let processInstruction state =
            let getRegister r =
                match state.registers |> Map.tryFind r with
                | Some v -> v
                | None -> 0L

            let getValue (v:Value) =
                match v with
                | Number n -> n
                | Register r -> getRegister r

            match program.[state.pointer] with
            | SetVal (r, v) ->
                { state with registers = state.registers |> Map.add r (getValue v); pointer = state.pointer + 1 }
            | Mul (r, v) ->
                let newValue = (getRegister r) * (getValue v)
                { state with
                    registers = state.registers |> Map.add r newValue;
                    mulCalled = state.mulCalled + 1;
                    pointer = state.pointer + 1 }
            | Jnz (v, o) when (getValue v) <> 0L ->
                { state with pointer = state.pointer + (getValue o |> int) }
            | Sub (r, v) ->
                let newValue = (getRegister r) - (getValue v)
                { state with registers = state.registers |> Map.add r newValue; pointer = state.pointer + 1 }
            | _ ->
                { state with pointer = state.pointer + 1 }

        if zero.pointer = program.Length
        then zero.mulCalled
        else
            let zero' = processInstruction zero
            run program zero'

    let solve() =
        let program =
            File.ReadAllLines("..\..\Input\day23.txt")
            |> Array.map parseLine

        let registersZero = Map.empty<RegisterName, int64>
        let stateZero = { registers = registersZero; queue = []; state = Running; pointer = 0; valuesSent = 0; mulCalled = 0 }

        printfn "part 1 = %A" <| run program stateZero
        printfn "part 2 = %A" <| 0
