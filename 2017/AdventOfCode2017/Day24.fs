module Day24

open System
open System.IO

type Connection = Left | Right

[<CustomEquality; NoComparison>]
type Component =
    { Port1: int; Port2: int; Connection: Connection }
    override this.Equals(obj) =
        match obj with
        | :? Component as other -> this.Port1 = other.Port1 && this.Port2 = other.Port2
        | _ -> false
    override this.GetHashCode() =
        this.Port1 * 33 + this.Port2

type Solution = { Current: Component list; Remaining: Component list; Modified: bool }

let parseInt s =
    match Int32.TryParse(s) with
    | (true, n) -> n
    | _ -> failwith (sprintf "illegal input: %s" s)

let parseLine (line:string) =
    let ports = line.Split('/')
    if ports.Length = 2
    then {
            Port1 = parseInt ports.[0];
            Port2 = parseInt ports.[1];
            Connection = Left
         }
    else failwith (sprintf "illegal input: %s" line)

let addZeros solution =
    let addNonZeros c =
        {
            Current = [c];
            Remaining = List.filter ((<>) c) solution;
            Modified = false
        }
    solution
    |> List.filter (fun c -> c.Port1 = 0)
    |> List.map addNonZeros

let findNext solutions =
    let impl solution =
        let current = List.head solution.Current
        let pins = if current.Connection = Left then current.Port2 else current.Port1
        let newConnections =
            solution.Remaining
            |> List.filter (fun r -> r.Port1 = pins || r.Port2 = pins)
            |> List.map (fun c -> {c with Connection = if c.Port1 = pins then Left else Right})

        if List.isEmpty newConnections
        then
            [{solution with Modified = false}]
        else
            newConnections
            |> List.map (fun c ->
                {
                    Current = c :: solution.Current;
                    Remaining = List.filter ((<>) c) solution.Remaining;
                    Modified = true
                })

    solutions |> List.collect impl

let rec findAll solutions =
    let result = findNext solutions
    if List.exists (fun s -> s.Modified) result
    then findAll result
    else result

let sumStrength s = s.Current |> List.sumBy (fun c -> c.Port1 + c.Port2)

let solve() =
    let allSolutions =
        File.ReadAllLines("..\..\Input\day24.txt")
        |> Array.map parseLine
        |> Array.toList
        |> addZeros
        |> findAll

    let longestLength = allSolutions |> List.map (fun s -> List.length s.Current) |> List.max
    let longestSolutions = allSolutions |> List.filter (fun s -> longestLength = List.length s.Current)

    let highestStrength = List.map sumStrength >> List.sortDescending >> List.head

    printfn "part 1 = %d" <| highestStrength allSolutions
    printfn "part 2 = %d" <| highestStrength longestSolutions
