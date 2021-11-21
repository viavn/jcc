import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate, CanLoad {
  constructor(
    private router: Router,
    private authService: AuthService,
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | boolean {

    return this.verifyAuthentication();
  }

  canLoad(route: Route): Observable<boolean> | Promise<boolean> | boolean {
    return this.verifyAuthentication();
  }

  private verifyAuthentication() {
    if (this.authService.isUserInSessionStorage()) {
      return true;
    }

    this.router.navigate(['/login']);
    return false;
  }
}
