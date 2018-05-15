module Day20

    open System.IO
    open System
    open System.Text.RegularExpressions

    type Coordinate =
        { X: int; Y: int; Z: int }
    with
        static member (+) (a, b) = { X = a.X + b.X; Y = a.Y + b.Y; Z = a.Z + b.Z }

    type Particle = { number: int; position: Coordinate; velocity: Coordinate; acceleration: Coordinate }

    let toCoordinate (s: string) =
        let tokens = s.Substring(1, s.Length - 2).Split(',') |> Array.map int
        { X = tokens.[0]; Y = tokens.[1]; Z = tokens.[2] }

    let toParticle number triplet =
        match triplet with
        | [ p; v; a ] -> { number = number; position = toCoordinate p; velocity = toCoordinate v; acceleration = toCoordinate a }
        | _ -> failwith "incorrect input"

    let parseLine number (line: string) =
        [ for m in Regex.Matches(line, "<.+?>") -> m.Value ]
        |> toParticle number

    let manhattan p =
        abs p.X + abs p.Y + abs p.Z

    let tick p =
        let v' = p.velocity + p.acceleration
        let p' = p.position + v'
        { p with position = p'; velocity = v' }

    let part1 =
        let step state =
            let next = state |> List.map tick
            Some (List.minBy (fun p -> manhattan p.position) next, next)

        Seq.unfold step

    let part2 =
        let step state =
            let next = state |> List.map tick
            let collisions =
                next
                |> List.countBy (fun p -> p.position)
                |> List.where (fun c -> (snd c) > 1)
                |> List.map fst
            let filtered =
                next
                |> List.where (fun p -> not <| List.contains p.position collisions)
            Some (List.length filtered, filtered)

        Seq.unfold step

    let solve() =
        let input =
            File.ReadAllLines("..\..\Input\day20.txt")
            |> Seq.mapi parseLine
            |> List.ofSeq

        printfn "part 1 = %A" <| (input |> part1 |> Seq.skip 300 |> Seq.head |> fun p -> p.number)
        printfn "part 2 = %A" <| (input |> part2 |> Seq.skip 300 |> Seq.head)
