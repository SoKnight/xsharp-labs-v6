module Task2

// бинарное дерево
type Tree =
    | Node of string * Tree * Tree
    | Empty

// pretty-printer для деревьев :o
let rec walkTreeToPrint (prefix: string) (tree: Tree): string =
    match tree with
    | Node (data: string, left: Tree, right: Tree) ->
        let nextPrefix = prefix |> sprintf "%s   "
        let mappedChildren = [left; right] |> List.map (walkTreeToPrint nextPrefix) |> List.filter (fun str -> str.Length <> 0)
        let childContent = mappedChildren |> String.concat "\n"

        if (childContent.Length <> 0) then
            sprintf "%s%s:\n%s" prefix data childContent
        else
            sprintf "%s%s" prefix data
    | Empty -> ""

// упрощённая функция для печати дерева в красивом виде
let printTree (tree: Tree): string = walkTreeToPrint "" tree

// определяет, является ли узел пустым
let isTreeEmpty (tree: Tree): bool = match tree with | Empty -> true | _ -> false

// определяет, является ли узел листом
let isTreeLeaf (tree: Tree): bool = match tree with | Node(a, left, right) -> (isTreeEmpty left) && (isTreeEmpty right) | _ -> false

// определяет, содержит ли узел два листа
let hasTwoLeafs (tree: Tree): bool = match tree with | Node(a, left, right) -> (isTreeLeaf left) && (isTreeLeaf right) | _ -> false

// ищет узлы дерева, подходящие под условие задачи
let rec findSuitableNodes (tree: Tree): List<Tree> =
    if (isTreeLeaf tree) then 
        []
    else if (hasTwoLeafs tree) then 
        [tree]
    else
        match tree with
        | Node(a, left: Tree, right: Tree) -> [left; right] |> List.fold (fun acc item -> findSuitableNodes item |> List.append acc) []
        | Empty -> []

// извлекает данные из узла
let extractData (tree: Tree): string =
    match tree with
    | Node(data: string, l, r) -> data
    | Empty -> ""

// форматирует один из найденных узлов
let formatFoundNode (tree: Tree): string =
    match tree with
    | Node(data: string, left: Tree, right: Tree) -> sprintf "'%s' (left: '%s', right: '%s')" data (extractData left) (extractData right)
    | Empty -> ""

let run =
    printfn "Задание 2:\nСформировать список из узлов с двумя листьями.\nУзел является листом, если у него нет ни левого, ни правого поддерева.\n"

    let tree: Tree =
        Node("root", 
            Node("node#1",
                Node("leaf#1", Empty, Empty),
                Node("leaf#2", Empty, Empty)
            ),
            Node("node#2",
                Node("node#3", 
                    Node("leaf#3", Empty, Empty),
                    Node("node#4",
                        Node("leaf#4", Empty, Empty),
                        Node("leaf#5", Empty, Empty)
                    )
                ),
                Node("node#5", 
                    Node("leaf#6", Empty, Empty),
                    Empty
                )
            )
        )

    printfn "[Исходное дерево]\n%s\n" (printTree tree)

    let found = findSuitableNodes tree
    if (found.IsEmpty) then
        printfn "Узлы с двумя листьями не найдены!"
    else
        printfn "[Узлы с двумя листьями]"
        for item in found do
            printfn "- %s" (formatFoundNode item)

    0