export interface BatchModuleDisplayDto {
    batchId:number;
    batchName:string;
    capacity:number;
    technology: string;
    batch_Start: string;
    batch_End: string;
    moduleName: string[];
    level: string;
    certification_Type: string;
}
