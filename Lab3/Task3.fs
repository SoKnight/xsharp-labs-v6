module Task3

open System.IO

let run =
    printfn "Задание 3:\nВывести первый по алфавиту файл в указанном каталоге.\n"

    printf "Введите путь к директории: "
    let userInput = System.Console.ReadLine()

    if (Directory.Exists(userInput)) then
        let fileNameOffset = userInput.Length + 1;
        let files = List.map (fun (entry: string) -> entry.Substring(fileNameOffset)) (Array.toList (Directory.GetFiles(userInput)));

        if (files.Length <> 0) then
            let sorted = List.sortBy (fun (name: string) -> name.ToLower()) files;
            printfn "Первый по алфавиту файл в каталоге: '%s'" sorted[0]
        else
            printfn "Указанная директория не содержит файлов!"
    else
        printfn "Указанная директория не существует!"
    0