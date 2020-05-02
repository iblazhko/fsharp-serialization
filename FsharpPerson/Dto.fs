module FsharpPerson.Dto

open System
open ResultComputationExpression
open Domain

type DtoError =
    | ValidationError of string
    | DeserializationException of Exception

type Person = {
    FirstName: string
    LastName: string
    Birthdate: Nullable<DateTime>
}

module OptionalBirthdate =
    let fromDomain (date: Domain.Birthdate option) =
        match date with
        | Some d -> Nullable<DateTime>(d |> Birthdate.value)
        | None -> Nullable<DateTime>()

    let toDomain (dto: Nullable<DateTime>): Result<Domain.Birthdate option,string> =
        if dto.HasValue then
            let dateOrError = dto.Value |> Birthdate.create "Birthdate"
            match dateOrError with
            | Ok d -> Ok (Some d)
            | Error e -> Error e
        else
            Ok None

module Person =
    let fromDomain (person: Domain.Person) : Person =
        {
            FirstName = person.FirstName |> String50.value
            LastName = person.LastName |> String50.value
            Birthdate = person.Birthdate |> OptionalBirthdate.fromDomain
        }

    let toDomain (dto: Person): Result<Domain.Person,string> =
        result {
            let! firstName = dto.FirstName |> String50.create "FirstName"
            let! lastName = dto.LastName |> String50.create "LastName"
            let! birthdate = dto.Birthdate |> OptionalBirthdate.toDomain
            return {
                FirstName = firstName
                LastName = lastName
                Birthdate = birthdate
            }
        }

    let jsonFromDomain (person: Domain.Person) =
        person
        |> fromDomain
        |> Json.serialize

    let jsonToDomain jsonString : Result<Domain.Person,DtoError> =
        result {
            let! deserializedValue =
                jsonString
                |> Json.deserialize
                |> Result.mapError DeserializationException
            let! domainValue =
                deserializedValue
                |> toDomain
                |> Result.mapError ValidationError
            return domainValue
        }
