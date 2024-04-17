import { Injectable } from '@angular/core';
import * as CryptoJs from 'crypto-js';

@Injectable({
  providedIn: 'root'
})
export class EncryptionService {

  encryptSecretKey = "1203199320052021"; //adding secret key
  constructor() { }
  encryptRequest(data:any):string
  {
    const encryptedData = CryptoJs.AES.encrypt(JSON.stringify(data),this.encryptSecretKey).toString();
    return encryptedData;
  }
}
