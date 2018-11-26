import { Injectable } from "@angular/core";
import { Router, CanActivate } from "@angular/router";
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private jwtHelper: JwtHelperService, private router: Router) {
    }

    canActivate() {
        var token = localStorage.getItem("jwt");

        if (token && !this.jwtHelper.isTokenExpired(token)) {

            return true;
        }
        this.router.navigate(["login"]);
        return false;
    }

    getIsLoggedIn() {
        var token = localStorage.getItem("jwt");

        if (token && token != "undefined"  && !this.jwtHelper.isTokenExpired(token)) {

            return true;
        }
        return false;
    }

    logOut(): void {
        localStorage.removeItem("jwt");
    }

    getUserName(): string {
        return localStorage.getItem("userName");
    }
}
