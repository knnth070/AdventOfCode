module Day21

    open System.IO
    open System

    let explode (square:string) =
        let input = square.Split('/')
        let size = input.Length
        Array2D.init size size (fun i j -> input.[i].[j])

    let pack (input:char[,]) =
        let lines = seq {
            for i in 0 .. input.GetUpperBound(0) do
                yield input.[i, *] |> String
        }
        String.Join("/", lines)

    let parseLine (line:string) =
        match line.Split(' ') with
        | [| input; _; output |] -> input, (explode output)
        | _ -> failwith "invalid input"

    let transpose (input:char[,]) =
        let size = input.GetUpperBound(0) + 1
        Array2D.init size size (fun i j -> input.[j,i])

    let flip (input:char[,]) =
        let upperBound = input.GetUpperBound(0)
        let output = Array2D.create (upperBound + 1) (upperBound + 1) ' '
        for r in 0 .. upperBound do
            output.[upperBound - r, *] <- input.[r, *]
        output

    let map book square =
        let s0 = square |> transpose
        let s1 = s0 |> flip
        let s2 = s1 |> transpose
        let s3 = s2 |> flip
        let s4 = s3 |> transpose
        let s5 = s4 |> flip
        let s6 = s5 |> transpose

        [square; s0; s1; s2; s3; s4; s5; s6] |> List.pick (fun x -> Map.tryFind (pack x) book)

    let transform (book:Map<string, char[,]>) (grid:char[,]) =
        let gridSize = grid.GetUpperBound(0) + 1
        let squareSize = if (gridSize % 2) = 0 then 2 else 3
        let newSize = if (gridSize % 2) = 0 then gridSize * 3 / 2 else gridSize * 4 / 3
        let output = Array2D.create newSize newSize ' '

        let rec impl i j =
            let i' = i * (squareSize + 1) / squareSize
            let j' = j * (squareSize + 1) / squareSize
            let square = grid.[i .. i + (squareSize - 1), j .. j + (squareSize - 1)]
            output.[i' .. i' + squareSize, j' .. j' + squareSize] <- square |> map book
            if i + squareSize >= gridSize
            then
                if j + squareSize >= gridSize
                then output
                else impl 0 (j + squareSize)
            else
                impl (i + squareSize) j

        impl 0 0

    let countPixels =
        Seq.cast<char> >> Seq.sumBy (fun c -> if c = '#' then 1 else 0)

    let solve() = 
        let startGrid = ".#./..#/###" |> explode
        let book =
            File.ReadAllLines("..\..\Input\day21.txt")
            |> Array.map parseLine
            |> Map.ofArray

        let part1 = List.fold (fun grid _ -> transform book grid) startGrid [1..5]
        let part2 = List.fold (fun grid _ -> transform book grid) part1 [6..18]
    
        printfn "part 1 = %A" <| countPixels part1
        printfn "part 2 = %A" <| countPixels part2
