module Task1

open System

let rec removeExtraSpaces (input: string): string =
    if (input.Contains "  ") then
        removeExtraSpaces (input.Replace ("  ", " "))
    else
        input

let findMax number =
    let digits = seq {
        for ch in string(number).ToCharArray() do
            if (int(ch) >= 48 && int(ch) <= 57) then
                yield int(string(ch))
    }
    
    Seq.max(digits)

let run =
    printfn "Задание 1:\nПолучить последовательность из максимальных цифр натуральных чисел, содержащихся в исходной последовательности.\n"

    printfn "Введите числа через пробел:"
    let userInput = System.Console.ReadLine()
    
    if (userInput.Length = 0) then
        printfn "Результат: []"
    else
        let rawParts = removeExtraSpaces(userInput.Trim()).Split(' ') |> Array.toSeq

        let numbers = seq {
            for arg in rawParts do
                match Int64.TryParse arg with
                | true, number when number > 0 -> yield number
                | _ -> printfn "'%s' не является натуральным числом!" arg
        }
        
        let result = numbers |> Seq.map findMax
        printfn "Результат: %A" (Seq.toList result)
    0