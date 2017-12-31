module Day16

    open System.IO

    type Move =
        | Spin of int
        | Exchange of int * int
        | Partner of char * char

    let parseMove move (arg:string) =
        match move with
        | 's' ->
            arg |> int |> Spin
        | 'x' ->
            let tokens = arg.Split('/')
            let a = int tokens.[0]
            let b = int tokens.[1]
            (a, b) |> Exchange
        | 'p' ->
            let a = arg.[0]
            let b = arg.[2]
            (a, b) |> Partner
        | _ ->
            failwith "illegal move"

    let parseInput (line: string) =
        line.Split(',')
        |> Seq.map (fun m -> parseMove m.[0] (m.Substring(1)))
    
    let dance move (programs: char []) =
        let findIndex p =
            programs |> Array.findIndex ((=) p)

        let swap a b (p: char []) =
            let q = Array.copy p
            let temp = q.[a]
            q.[a] <- q.[b]
            q.[b] <- temp
            q

        match move with
        | Spin s ->
            let len = programs.Length
            programs
            |> Array.skip (len - s)
            |> Array.append <| (Array.take (len - s) programs)
        | Exchange (a, b) ->
            programs |> swap a b
        | Partner (a, b) ->
            let i = findIndex a
            let j = findIndex b
            programs |> swap i j

    let findRepeatLength (initial: char []) moves =
        let rec impl current moves acc =
            let newState = Seq.fold (fun p m -> dance m p) current moves

            if newState = initial
            then acc
            else impl newState moves (acc + 1)

        impl (Array.copy initial) moves 1

    let rec fullDance programs moves repetitions =
        let newState = Seq.fold (fun p m -> dance m p) programs moves

        if repetitions = 0
        then programs
        else fullDance newState moves (repetitions - 1)

    let solve() =
        let input =
            File.ReadAllLines("..\..\Input\day16.txt").[0]
            |> parseInput

        let programs = [| 'a'..'p' |]

        let part1 = fullDance programs input 1 |> System.String

        let repeat = findRepeatLength programs input
        let modulo = 1_000_000_000 % repeat
        let part2 = fullDance programs input modulo |> System.String

        printfn "part 1 = %A" <| part1
        printfn "part 2 = %A" <| part2
