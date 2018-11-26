import { Component, Inject } from '@angular/core';
import { LoginModel } from "../../../../Models/TypescriptModels/LoginModel";
import { Router } from "@angular/router";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import * as sha256 from "fast-sha256";

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class Login {
    hidePassword = true;
    invalidLogin = false;

    credentials = new LoginModel();

    constructor(private router: Router, private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    login() {

        this.http.post(this.baseUrl + "api/auth/login", this.credentials, {
            headers: new HttpHeaders({
                "Content-Type": "application/json"
            })
        }).subscribe(response => {
            let token = (<any>response).Token;
            localStorage.setItem("jwt", token);
            localStorage.setItem("userName", this.credentials.UserName);
            this.invalidLogin = false;
            this.router.navigate(["/"]);
        }, err => {
            this.invalidLogin = true;
        });
    }
}
