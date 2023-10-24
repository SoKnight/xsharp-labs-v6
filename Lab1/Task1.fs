module Task1

open System

let run =
    printfn "Задание 1:\nСформировать список из чисел, противоположных вводимым значениям.\n"

    printfn "Введите числа через пробел:"
    let userInput = System.Console.ReadLine()
    
    if (userInput.Length = 0) then
        printfn "Результат: []"
    else
        let result = [ 
            for arg in (userInput.Replace ('.', ',')).Split(' ') do 
                match Double.TryParse arg with
                | true, number -> yield -number
                | _ -> ignore
        ]

        printfn "Результат: %A" result
    0