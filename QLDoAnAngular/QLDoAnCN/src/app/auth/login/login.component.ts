import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../api/auth.service';
import { LoginResponse } from '../../models/login-response.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = '';
  password = '';
  errorMessage = '';

  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  onSubmit() {
    const credentials = {
      tenDangNhap: this.username,
      matKhau: this.password
    };

    this.authService.login(credentials).subscribe({
      next: (response: LoginResponse) => {
        console.log('Đăng nhập thành công!', response);
        this.errorMessage = '';
        localStorage.setItem('userRole', response.vaiTro);
        localStorage.setItem('username', response.tenDangNhap);
        localStorage.setItem('userId', response.maNguoiDung);
        alert('Đăng nhập thành công');
        // // this.router.navigate(['/khoa']);
        // this.router.navigate(['/bomon']);
        this.router.navigate(['/dashboard']); 
      },
      error: (error) => {
        console.error('Lỗi đăng nhập:', error);
        this.errorMessage = 'Tên đăng nhập hoặc mật khẩu không đúng.';
        if (error.status === 401) {
          this.errorMessage = 'Tên đăng nhập hoặc mật khẩu không đúng.';
        } else {
          this.errorMessage = 'Đã xảy ra lỗi khi đăng nhập. Vui lòng thử lại sau.';
        }
      }
    });
  }
}