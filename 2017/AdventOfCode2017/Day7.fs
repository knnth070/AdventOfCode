module Day7

    open System.IO
    open System

    let parseLine (line:string) =
        let tokens = line.Split(' ')
        let root = tokens.[0]
        let weight = tokens.[1].Substring(1, tokens.[1].Length - 2) |> int
        let indexOfArrow = line.IndexOf("->")
        let children =
            if indexOfArrow >= 0
            then Some (line.Substring(indexOfArrow + 3).Split([|','; ' '|], StringSplitOptions.RemoveEmptyEntries) |> Seq.ofArray)
            else None

        root, weight, children
    
    let findRoot nodes =
        let roots = nodes |> Seq.map (fun (r, _, _) -> r)
        let children = nodes |> Seq.choose (fun (_, _, c) -> c) |> Seq.concat

        roots |> Seq.pick (fun r -> if Seq.contains r children then None else Some r)

    let rec calculateWeight nodes name =
        let _, weight, children = nodes |> Seq.filter (fun (r,_,_) -> r = name) |> Seq.head
        weight + match children with
                    | Some c -> Seq.fold (fun w n -> w + (calculateWeight nodes n)) 0 c
                    | None -> 0


    let printNode nodes parent name =
        let node = Seq.find (fun (r, _, _) -> r = name) nodes
        let _, weight, _ = node

        printfn "%s -> %s: %d (%d)" parent name weight (calculateWeight nodes name)

    let rec findUnbalanced nodes parent name =
        let node = Seq.find (fun (r, _, _) -> r = name) nodes
        let _, _, children = node

        match children with
        | None -> ()
        | Some c ->
            let wc = c |> Seq.map (calculateWeight nodes)
            if (Seq.max wc <> Seq.min wc) then
                printfn "Unbalanced:"
                Seq.iter (printNode nodes parent) c
            c |> Seq.iter (findUnbalanced nodes name)

    let solve() =
        let input = File.ReadAllLines("..\..\Input\day7.txt") |> Seq.map parseLine
        let root = findRoot input

        printfn "root node = %A" root
        findUnbalanced input "" root
