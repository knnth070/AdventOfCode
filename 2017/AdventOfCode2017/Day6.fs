module Day6

    open System.IO
    open System

    let redistribute banks =
        let rec spread (b:int[]) index i =
            match i with
            | 0 -> ()
            | _ ->
                b.[index % b.Length] <- b.[index % b.Length] + 1
                spread b (index + 1) (i - 1)

        let rec impl b visited cycle firstOccurence =
            let max = Array.max b
            let index = Array.findIndex ((=) max) b
            b.[index] <- 0
            spread b (index + 1) max
            
            if List.contains b visited
            then
                if firstOccurence > 0
                then firstOccurence, cycle
                else impl b [Array.copy b] 1 cycle
            else impl b ((Array.copy b)::visited) (cycle + 1) firstOccurence
            
        impl banks [Array.copy banks] 1 0

    let solve() =
        let input =
            (File.ReadAllLines("..\..\Input\day6.txt").[0]).Split('\t')
            |> Array.map int

        printfn "first, second = %A" (redistribute input)
