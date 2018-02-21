/* Options:
Date: 2017-11-08 04:44:10
Version: 5.00
Tip: To override a DTO option, remove "//" prefix before updating
BaseUrl: http://localhost:63198/

//GlobalNamespace: 
//MakePropertiesOptional: True
//AddServiceStackTypes: True
//AddResponseStatus: False
//AddImplicitVersion: 
//AddDescriptionAsComments: True
//IncludeTypes: 
//ExcludeTypes: 
//DefaultImports: 
*/


export interface IReturn<T>
{
    createResponse() : T;
}

export interface IReturnVoid
{
    createResponse() : void;
}

export class HelloResponse
{
    result: string;
}

// @Route("/hello")
// @Route("/hello/{Name}")
export class Hello implements IReturn<HelloResponse>
{
    name: string;
    createResponse() { return new HelloResponse(); }
    getTypeName() { return "Hello"; }
}

export class ClientsResponse
{
    response: string;
    clients: Array<object>;
}

export class Client implements IReturn<ClientsResponse>
{
    name: string;
    id: number;
    createResponse() { return new ClientsResponse(); }
    getTypeName() { return "Client"; }
}

export class LocationsResponse {
    cacheUpdated: any;
    locations: Array<object>;
}

export class Location implements IReturn<LocationsResponse>
{
    longitude: number;
    latitude: number;
    name: string;
    createResponse() { return new LocationsResponse(); }
    getTypeName() { return "Location"; }
}
