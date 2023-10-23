module Task2

open System

let removeMinus (userInput: string) =
    if (userInput.StartsWith "-") then
         userInput.Substring 1
     else
         userInput

let tryCountDigits (input: string): bool * int =
    if (input.Length <> 0 && not (input.StartsWith "0")) then
        let digits = [
            for ch in input.ToCharArray() do
                if (int(ch) >= 48 && int(ch) <= 57) then
                    yield ch
        ]
                
        if (digits.Length = input.Length) then
            (true, digits.Length)
        else
            (false, 0)
    else
        (false, 0)

let run =
    printfn "Задание 2:\nНайти количество цифр в записи натурального числа.\n"

    printf "Введите число: "
    let userInput = System.Console.ReadLine()

    if (userInput.Length = 0) then
        printfn "Вам необходимо ввести какое-то натуральное число!"
    else
        let stripped = removeMinus userInput

        match tryCountDigits stripped with
        | (true, count) -> printfn "Количество цифр в числе: %i" count
        | _ -> printfn "Введённое значение не является числом!"
    0