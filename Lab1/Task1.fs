module Task1

let run =
    printfn "Задание 1:\nСформировать список из чисел, противоположных вводимым значениям.\n"

    printfn "Введите числа через пробел:"
    let userInput = System.Console.ReadLine()
    
    if (userInput.Length = 0) then
        printfn "Результат: []"
    else
        let result = [ for arg in userInput.Split(' ') do yield -int(arg); ]
        printfn "Результат: %A" result
    0