module Knothash

    open System

    let private parseInput (line:string) =
        line.Split(',')
        |> List.ofSeq
        |> List.map int

    let private parseAsciiInput (line:string) =
        line
        |> List.ofSeq
        |> List.map int
        |> List.append <| [17; 31; 73; 47; 23]

    let private single input =
        let shift circle steps =
            (List.skip steps circle) @ (List.take steps circle)

        let impl circle length skip start =
            let reversed =
                circle
                |> List.take length
                |> List.rev
            let result = List.append reversed (List.skip length circle)
            let len = List.length circle
            let skipCount = (length + skip) % len
            (shift result skipCount), skip + 1, (len + start - skipCount) % len

        let circle, _, start =
            input
            |> List.fold (fun (circle, skip, start) length -> impl circle length skip start) ([0 .. 255], 0, 0)

        shift circle start

    let private dense l =
        let rec doubleList n l =
            match n with
            | 0 -> l
            | _ -> doubleList (n - 1) (l @ l)

        let sparse =
            l
            |> doubleList 6 // 2^6 = 64
            |> single

        String.Join("",
            seq {
                for offset in [0..16..255] do
                    yield
                        sparse
                        |> List.skip offset
                        |> List.take 16
                        |> List.fold (^^^) 0
            }
            |> Seq.map (sprintf "%02x")
        )

    let hash = parseInput >> single
    let densehash = parseAsciiInput >> dense
