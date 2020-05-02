module FsharpPerson.ResultComputationExpression

type ResultBuilder() =
    member this.Return(x) = Ok x
    member this.Bind(x,f) = Result.bind f x
let result = ResultBuilder()
