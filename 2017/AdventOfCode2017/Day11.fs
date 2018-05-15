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

    let move position = function
        | "n" -> { position with y = position.y + 1; z = position.z - 1 }
        | "ne" -> { position with x = position.x + 1; z = position.z - 1 }
        | "se" -> { position with x = position.x + 1; y = position.y - 1 }
        | "s" -> { position with y = position.y - 1; z = position.z + 1 }
        | "sw" -> { position with x = position.x - 1; z = position.z + 1 }
        | "nw" -> { position with x = position.x - 1; y = position.y + 1 }
        | _ -> position

    let solve() =
        let result =
            File.ReadAllLines("..\..\Input\day11.txt").[0]
            |> parseInput
            |> Seq.scan move origin
            |> Seq.map (distance origin)

        printfn "part 1 = %A" <| Seq.last result
        printfn "part 2 = %A" <| Seq.max result
