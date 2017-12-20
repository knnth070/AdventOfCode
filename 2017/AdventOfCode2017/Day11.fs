module Day11

    // https://www.redblobgames.com/grids/hexagons/

    open System.IO
    open System

    type Coordinate = { x: int; y: int; z: int }

    let parseInput (line:string) =
        line.Split(',')
        |> Seq.ofArray

    let distance a b =
        ((abs (a.x - b.x)) + (abs (a.y - b.y)) + (abs (a.z - b.z))) / 2

    let origin = { x = 0; y = 0; z = 0 }

    let move position direction =
        let newPosition =
            match direction with
            | "n" -> { position with y = position.y + 1; z = position.z - 1 }
            | "ne" -> { position with x = position.x + 1; z = position.z - 1 }
            | "se" -> { position with x = position.x + 1; y = position.y - 1 }
            | "s" -> { position with y = position.y - 1; z = position.z + 1 }
            | "sw" -> { position with x = position.x - 1; z = position.z + 1 }
            | "nw" -> { position with x = position.x - 1; y = position.y + 1 }
            | _ -> position
        newPosition, (distance origin newPosition)

    let solve() =
        let result =
            File.ReadAllLines("..\..\Input\day11.txt").[0]
            |> parseInput
            |> Seq.scan (fun (s, _) d -> move s d) (origin, 0)

        let part1 = result |> Seq.last |> snd
        let part2 = result |> Seq.map snd |> Seq.max

        printfn "part 1 = %A" part1
        printfn "part 2 = %A" part2
