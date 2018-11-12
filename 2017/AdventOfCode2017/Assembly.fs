module Assembly

    open System

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
        | Jnz of Value * Value
        | Sub of RegisterName * Value

    type RunningState = Running | Waiting

    type State =
        {
            registers: Map<RegisterName, int64>;
            queue: int64 list;
            state: RunningState;
            pointer: int;
            valuesSent: int;
            mulCalled: int;
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
        | "jnz" -> Jnz (tokens.[1] |> toValue, tokens.[2] |> toValue)
        | "sub" -> Sub (tokens.[1] |> toRegisterName, tokens.[2] |> toValue)
        | _ -> failwith "invalid instruction in input"
