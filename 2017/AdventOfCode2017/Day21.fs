module Day21

    open System.IO
    open System

    let parseLine (line:string) =
        let [| input; _; output |] = line.Split(' ')
        input, output

    let explode (square:string) =
        square.Split('/')

    let pack (lines:string[]) =
        String.Join("/", lines)

    let rotate orig =
        let square = explode orig
        let rotate2x2() =
            let line0 = [| square.[1].[0]; square.[0].[0] |] |> System.String
            let line1 = [| square.[1].[1]; square.[0].[1] |] |> System.String
            pack [| line0; line1 |]
        let rotate3x3() =
            let line0 = [| square.[2].[0]; square.[1].[0]; square.[0].[0] |] |> System.String
            let line1 = [| square.[2].[1]; square.[1].[1]; square.[0].[1] |] |> System.String
            let line2 = [| square.[2].[2]; square.[1].[2]; square.[0].[2] |] |> System.String
            pack [| line0; line1; line2 |]
        match square.Length with
        | 2 -> rotate2x2()
        | 3 -> rotate3x3()
        | _ -> failwith "illegal square size"

    let flip orig =
        explode orig
        |> Array.map (fun x -> x.ToCharArray() |> Array.rev |> System.String)
        |> pack

    let show square =
        square
        |> Array.iter (printfn "%s")

    let map book square =
        let rotates = [ square; rotate square; rotate square |> rotate; rotate square |> rotate |> rotate ]
        let flips = rotates |> List.map flip
        (rotates @ flips) |> List.pick (fun x -> Map.tryFind x book)

    let transform (book:Map<string,string>) (grid:string[]) =
        let rec transform3x3 (output:string[]) x y =
            let outY = y / 3 * 4
            let line0 = [| grid.[y].[x]; grid.[y].[x + 1]; grid.[y].[x + 2] |] |> System.String
            let line1 = [| grid.[y + 1].[x]; grid.[y + 1].[x + 1]; grid.[y + 1].[x + 2] |] |> System.String
            let line2 = [| grid.[y + 2].[x]; grid.[y + 2].[x + 1]; grid.[y + 2].[x + 2] |] |> System.String
            let newSquare = [| line0; line1; line2 |] |> pack |> map book |> explode
            output.[outY] <- output.[outY] + newSquare.[0]
            output.[outY + 1] <- output.[outY + 1] + newSquare.[1]
            output.[outY + 2] <- output.[outY + 2] + newSquare.[2]
            output.[outY + 3] <- output.[outY + 3] + newSquare.[3]
            if x + 3 >= grid.Length
            then 
                if y + 3 >= grid.Length
                then output
                else transform3x3 output 0 (y + 3)
            else
                transform3x3 output (x + 3) y

        let rec transform2x2 (output:string[]) x y =
            let outY = y / 2 * 3
            let line0 = [| grid.[y].[x]; grid.[y].[x + 1] |] |> System.String
            let line1 = [| grid.[y + 1].[x]; grid.[y + 1].[x + 1] |] |> System.String
            let newSquare = [| line0; line1 |] |> pack |> map book |> explode
            output.[outY] <- output.[outY] + newSquare.[0]
            output.[outY + 1] <- output.[outY + 1] + newSquare.[1]
            output.[outY + 2] <- output.[outY + 2] + newSquare.[2]
            if x + 2 >= grid.Length
            then 
                if y + 2 >= grid.Length
                then output
                else transform2x2 output 0 (y + 2)
            else
                transform2x2 output (x + 2) y

        if (grid.Length % 2) = 0
        then transform2x2 (Array.create (grid.Length * 3 / 2) "") 0 0
        else transform3x3 (Array.create (grid.Length * 4 / 3) "") 0 0

    let countPixels (grid: string[]) =
        grid
        |> Array.collect (fun s -> s.ToCharArray())
        |> Array.sumBy (fun c -> if c = '#' then 1 else 0)

    let rec iterate count f input =
        if count = 0
        then input
        else iterate (count - 1) f (f input)

    let solve() =
        let startGrid = ".#./..#/###" |> explode
        let book =
            File.ReadAllLines("..\..\Input\day21.txt")
            |> Array.map parseLine
            |> Map.ofArray

        let part1 = iterate 5 (transform book) startGrid
        let part2 = iterate 13 (transform book) part1

        printfn "part 1 = %A" <| countPixels part1
        printfn "part 2 = %A" <| countPixels part2
