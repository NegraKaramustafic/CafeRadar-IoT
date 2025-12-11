export interface CafeStatus {
    id:number;
    name:string;
    description:string;
    address:string;
    imageUrl:string;
    noiseLevel:string | null;
    lightLevel:string | null;
    measuredAtUtc:string | null;
}