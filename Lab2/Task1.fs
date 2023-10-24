module Task1

open System

let rec removeExtraSpaces (input: string): string =
    if (input.Contains "  ") then
        removeExtraSpaces (input.Replace ("  ", " "))
    else
        input

let findMax number =
    let digits = [
        for ch in string(number).ToCharArray() do
            if (int(ch) >= 48 && int(ch) <= 57) then
                yield int(string(ch))
    ]
    
    List.max(digits)

let run =
    printfn "Задание 1:\nПолучить список из максимальных цифр натуральных чисел, содержащихся в исходном списке.\n"

    printfn "Введите числа через пробел:"
    let userInput = System.Console.ReadLine()
    
    if (userInput.Length = 0) then
        printfn "Результат: []"
    else
        let rawParts = removeExtraSpaces(userInput.Trim()).Split(' ')
        let numbers = [ 
            for arg in rawParts do
                match Int64.TryParse arg with
                | true, number when number > 0 -> yield number
                | _ -> printfn "'%s' не является натуральным числом!" arg
        ]

        if (rawParts.Length = numbers.Length) then
            let result = List.map ( findMax ) numbers
            printfn "Результат: %A" result
        else
            printfn "Введённое значение не является перечислением натуральных чисел!"
    0