module Day18

    open System
    open System.IO

    type RegisterName = RegisterName of char

    type Value =
        | Register of RegisterName
        | Number of int64

    type Instruction = 
        | Snd of Value
        | SetVal of RegisterName * Value
        | Add of RegisterName * Value
        | Mul of RegisterName * Value
        | Mod of RegisterName * Value
        | Rcv of RegisterName
        | Jgz of Value * Value

    type RunningState = Running | Waiting

    type State = 
        {
            registers: Map<RegisterName, int64>;
            queue: int64 list;
            state: RunningState;
            pointer: int;
            valuesSent: int;
        }

    let parseInt s =
        match Int64.TryParse(s) with
        | true, i  -> Some i
        | _ -> None

    let toRegisterName (s:string) =
        s.[0] |> RegisterName

    let toValue s =
        match parseInt s with
        | Some i -> i |> Number
        | _ -> s.[0] |> RegisterName |> Register

    let parseLine (line:string) =
        let tokens = line.Split(' ')
        match tokens.[0] with
        | "snd" -> Snd (tokens.[1] |> toValue)
        | "set" -> SetVal (tokens.[1] |> toRegisterName, tokens.[2] |> toValue)
        | "add" -> Add (tokens.[1] |> toRegisterName, tokens.[2] |> toValue)
        | "mul" -> Mul (tokens.[1] |> toRegisterName, tokens.[2] |> toValue)
        | "mod" -> Mod (tokens.[1] |> toRegisterName, tokens.[2] |> toValue)
        | "rcv" -> Rcv (tokens.[1] |> toRegisterName)
        | "jgz" -> Jgz (tokens.[1] |> toValue, tokens.[2] |> toValue)
        | _ -> failwith "invalid instruction in input"

    let rec run (program:Instruction[]) zero one =
        let processInstruction this other =
            let getRegister r =
                match this.registers |> Map.tryFind r with
                | Some v -> v
                | None -> 0L

            let getValue (v:Value) =
                match v with
                | Number n -> n
                | Register r -> getRegister r

            match program.[this.pointer] with
            | Snd v ->
                let this' = { this with valuesSent = this.valuesSent + 1; pointer = this.pointer + 1 }
                let other' = { other with queue = other.queue @ [(getValue v)]; state = Running }
                this', other'
            | SetVal (r, v) ->
                let this' = { this with registers = this.registers |> Map.add r (getValue v); pointer = this.pointer + 1 }
                this', other
            | Add (r, v) ->
                let newValue = (getRegister r) + (getValue v)
                let this' = { this with registers = this.registers |> Map.add r newValue; pointer = this.pointer + 1 }
                this', other
            | Mul (r, v) ->
                let newValue = (getRegister r) * (getValue v)
                let this' = { this with registers = this.registers |> Map.add r newValue; pointer = this.pointer + 1 }
                this', other
            | Mod (r, v) ->
                let newValue = (getRegister r) % (getValue v)
                let this' = { this with registers = this.registers |> Map.add r newValue; pointer = this.pointer + 1 }
                this', other
            | Rcv r ->
                let this' =
                    match this.queue with
                    | [] ->
                        { this with state = Waiting }
                    | x::xs ->
                        { this with registers = this.registers |> Map.add r x; queue = xs; pointer = this.pointer + 1 }
                this', other
            | Jgz (v, o) when (getValue v) > 0L ->
                { this with pointer = this.pointer + (getValue o |> int) }, other
            | _ ->
                { this with pointer = this.pointer + 1 }, other

        if (zero.state, one.state) = (Waiting, Waiting)
        then one.valuesSent
        else
            let zero', one' = processInstruction zero one
            let one'', zero'' = processInstruction one' zero'
            run program zero'' one''


    let solve() =
        let input =
            File.ReadAllLines("..\..\Input\day18.txt")
            |> Seq.map parseLine
            |> Array.ofSeq

        let registersZero = Map.empty<RegisterName, int64> |> Map.add (toRegisterName "p") 0L
        let registersOne = Map.empty<RegisterName, int64> |> Map.add (toRegisterName "p") 1L

        let stateZero = { registers = registersZero; queue = []; state = Running; pointer = 0; valuesSent = 0 }
        let stateOne = { stateZero with registers = registersOne }
        printfn "Values sent = %A" <| run input stateZero stateOne
