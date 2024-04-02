import { Inject, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { AuthServiceService } from '../../service/auth-service.service';
import { Observable, map } from 'rxjs';
import { SetUser } from '../../interface/set-user';
import { ToastrService } from 'ngx-toastr';
import { UserStoreService } from '../../service/user-store.service';

export const authGuard: CanActivateFn =
  (route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | UrlTree => {
    const authService = inject(AuthServiceService);
    const roleService = inject(UserStoreService);
    const router = inject(Router);
    const toastr = inject(ToastrService);

    return authService.user$.pipe(
      map((user: SetUser) => {
        // console.log(user);
        if (user) {
          return true;
        } else {
          router.navigate(['/login']);
          return false;
        }
      })
    )
  };
