import { Component } from '@angular/core';
import { AppComponent } from '../app.component';
import { AuthService } from '../_services/auth.service';
import { User } from '../_models/user';

@Component({
  selector: 'app-headerLoggedIn',
  template: `
    <nav>
      <div class="nav">
        <img id="logo" class="linkLeft" src="/assets/images/socialmachine.png"  routerLink="/main">
        <a class="linkRight" [routerLink]="['/']" (click)="logOut()">Logout</a>
        <a class="linkRight" [routerLink]="['/profile']" >Profile</a>
      </div>
    </nav>
  `,
  styles: [`
  .nav{
    background-color: gray;
  }
  #logo{
    display: inline-block;
    width: 40px;
    margin-right: 10px;
    background-color: transparent;
  }
  #logo:hover{
    cursor: pointer;
  }
  a:link, a:visited{
    display: inline-block;
    border-radius: 5px;
    padding: 5px 5px 5px 5px;
    color: #eee;
    text-decoration: none;
  }
  .linkLeft{
    margin: 0px 5px 0px 5px;
    float: left;
  }
  .linkRight{
    margin: 0px 5px 0px 5px;
    float: right;
  }
  `]
})
export class HeaderLoggedInComponent {
  
  constructor(
    private auth: AuthService,
    private AppComponent: AppComponent)
  { 
    this.auth.currentUser.subscribe(x => this.currentUser = x)
  }
  
  
  currentUser: any
  user: User[] = []  

  logOut(){
    this.auth.logout()
    this.auth.currentUser.subscribe(x => this.currentUser = x );
    
    //ændrer headeren
    this.AppComponent.validateHeader()

  }

}
