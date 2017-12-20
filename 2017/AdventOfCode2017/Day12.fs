module Day12

    open System.IO
    open System

    let parseLine (line:string) =
        let tokens = line.Split([| "<->" |], StringSplitOptions.RemoveEmptyEntries)
        let programId = tokens.[0] |> int
        let communicatesWith = tokens.[1].Split(',') |> Seq.map int |> List.ofSeq

        programId, communicatesWith

    let rec findGroup programs programId =
        let rec impl found = 
            let newItems =
                found
                |> List.map (fun x ->
                                programs
                                |> List.filter (fun y -> List.contains x (snd y))
                                |> List.map fst
                )
                |> List.concat
            let unique = List.filter (fun x -> not <| List.contains x found) newItems
            if List.isEmpty unique
            then found
            else impl (found @ unique)

        impl [programId] |> List.sort

    let findAllGroups programs =
        let containsProgram x groups =
            groups
            |> Set.filter (List.contains x)
            |> Set.isEmpty
            |> not

        let rec impl programIds groups =
            match programIds with
            | [] -> groups
            | p::ps ->
                let newGroup =
                    if containsProgram p groups
                    then groups
                    else
                        let group = findGroup programs p
                        Set.add group groups
                impl ps newGroup
        
        impl (List.map fst programs) Set.empty

    let solve() =
        let input =
            File.ReadAllLines("..\..\Input\day12.txt")
            |> List.ofSeq
            |> List.map parseLine

        printfn "part 1 = %A" <| List.length (findGroup input 0)
        printfn "part 2 = %A" <| Set.count (findAllGroups input)
