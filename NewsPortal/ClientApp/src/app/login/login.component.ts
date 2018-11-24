import { Component, Inject } from '@angular/core';
import { LoginModel } from "../../../../Models/TypescriptModels/LoginModel";
import { Router } from "@angular/router";
import { HttpClient, HttpHeaders } from '@angular/common/http';

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
            let token = (<any>response).token;
            localStorage.setItem("jwt", token);
            this.invalidLogin = false;
            this.router.navigate(["/"]);
        }, err => {
            this.invalidLogin = true;
        });
    }
}
