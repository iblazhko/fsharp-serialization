open FsharpPerson

let section title =
    printfn "################################################################################"
    printfn "########## %s" title

[<EntryPoint>]
let main argv =
    printfn "Hello from F# World!"
    printfn ""

    section "Happy path"
    printfn "Let's start with a valid JSON representing a domain model:"
    let jsonString = """
    {
        "firstName": "Adam",
        "lastName": "Smith",
        "birthDate": "1980-01-02",
    }
    """
    printfn "%s" jsonString
    printfn ""

    printfn "Let's deserialize the JSON and convert it to domain model:"
    let personOrError = jsonString |> Dto.Person.jsonToDomain
    printfn "%A" personOrError
    printfn ""

    section "Happy path with optional values"
    printfn "Let's use a JSON that omits optional value in domain model:"
    let jsonString = """
    {
        "firstName": "Adam",
        "lastName": "Smith"
    }
    """
    printfn "%s" jsonString
    printfn ""

    printfn "Let's deserialize the JSON and convert it to domain model:"
    let personOrError = jsonString |> Dto.Person.jsonToDomain
    printfn "%A" personOrError
    printfn ""

    section "Domain validation error"
    printfn "Let's use a JSON that represents an invalid domain model:"
    let jsonString = """
    {
        "firstName": "",
        "lastName": "SmithSmithSmithSmithSmithSmithSmithSmithSmithSmithSmith",
        "birthDate": "1980-01-02",
    }
    """
    printfn "%s" jsonString
    printfn ""

    printfn "Let's try to deserialize the JSON and convert it to domain model:"
    let personOrError = jsonString |> Dto.Person.jsonToDomain
    printfn "%A" personOrError
    printfn ""

    section "JSON serialization error"
    printfn "Let's use an invalid JSON that cannot be deserialized:"
    let jsonString = """
    {
        "firstName": "Adam",
        "lastName": "Smith",
        "birthDate": "0000-00-00",
    }
    """
    printfn "%s" jsonString
    printfn ""

    printfn "Let's try to deserialize the JSON and convert it to domain model:"
    let personOrError = jsonString |> Dto.Person.jsonToDomain
    printfn "%A" personOrError
    printfn ""

    // The End
    0
