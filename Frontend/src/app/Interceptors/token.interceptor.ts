import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthServiceService } from '../service/auth-service.service';
import { catchError, mergeMap, take, throwError } from 'rxjs';
import { SetUser } from '../interface/set-user';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {

  const authService = inject(AuthServiceService);
  const toastr = inject(ToastrService);
  const router = inject(Router);
  authService.user$.pipe(take(1)).subscribe({
    next:user =>{
      if(user){
        //clone from coming request and add auth header
        req = req.clone({
          setHeaders:{
            Authorization: `Bearer ${user.jwt}`
          }
        })
      }
    }
  });

  return next(req)


  
};
