import {  HttpInterceptorFn} from '@angular/common/http';
import { inject } from '@angular/core';
import { EncryptDecryptService } from '../service/encrypt-decrypt.service';
import { EncryptionService } from '../service/encryption.service';

export const encryptDecryptAuthInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  const encryptDecryptService = inject(EncryptDecryptService);
  const encryptService = inject(EncryptionService);
  // debugger
  if (req.method === "GET") {
    if (req.url.indexOf("?") > 0) { // if request has query params this if block will execute
      let encriptURL = req.url.substring(0, req.url.indexOf("?") + 1) + encryptDecryptService.encryptUsingAES256(req.url.substring(req.url.indexOf("?") + 1, req.url.length));
      const cloneReq = req.clone({
        url: encriptURL
      });
      return next(cloneReq);
    }
    return next(req);
  } 
  else if (req.method === "POST") {
    if (req.body) {
      const cloneReq = req.clone({
        body: encryptDecryptService.encryptUsingAES256(req.body)
        // body:encryptService.encryptRequest(req.body)
      });
      return next(cloneReq);
    }
  }
  return next(req);
}

