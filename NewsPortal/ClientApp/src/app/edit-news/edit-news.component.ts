import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
    selector: 'edit-news',
    templateUrl: './edit-news.component.html'
})
export class EditNewsComponent implements OnInit {
    values: any;

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) { }

    ngOnInit() {
        let token = localStorage.getItem("jwt");
        this.http.get(this.baseUrl + "api/values", {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        }).subscribe(response => {
            this.values = response;
        }, err => {
            console.log(err)
        });
    }
}
