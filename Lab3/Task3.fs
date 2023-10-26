module Task3

open System.IO

let run =
    printfn "Задание 3:\nВывести первый по алфавиту файл в указанном каталоге.\n"

    printf "Введите путь к директории: "
    let userInput = System.Console.ReadLine()

    if (Directory.Exists(userInput)) then
        let fileNameOffset = userInput.Length + 1;
        let files = Directory.GetFiles(userInput) 
                    |> Array.toSeq 
                    |> Seq.map (fun (entry: string) -> entry.Substring(fileNameOffset))
                    |> Seq.sortBy (fun (name: string) -> name.ToLower());

        let enumerator = files.GetEnumerator();
        if (enumerator.MoveNext()) then
            printfn "Первый по алфавиту файл в каталоге: '%s'" enumerator.Current
        else
            printfn "Указанная директория не содержит файлов!"
    else
        printfn "Указанная директория не существует!"
    0