import axios from 'axios';

export const API_URL = 'http://localhost:5256/api';

// Configure axios defaults
axios.defaults.withCredentials = true;
axios.defaults.headers.common['Content-Type'] = 'application/json';

interface ErrorResponse {
  message?: string;
  errors?: string[];
}

interface LoginCredentials {
  email: string;
  password: string;
}

interface RegisterData {
  email: string;
  username: string;
  password: string;
  confirmPassword: string;
}

interface AuthResponse {
  token: string;
}

export const authService = {
  async login(email: string, password: string) {
    try {
      const response = await axios.post<AuthResponse>(`${API_URL}/auth/login`, { email, password });
      return response.data;
    } catch (error) {
      console.error('Login error:', error);
      if (axios.isAxiosError(error)) {
        const response = error.response?.data as ErrorResponse;
        throw new Error(response?.message || 'Failed to login. Please check your credentials and try again.');
      }
      throw new Error('Failed to login. Please check your credentials and try again.');
    }
  },

  async register(data: RegisterData): Promise<void> {
    try {
      console.log('Sending registration request to:', `${API_URL}/auth/register`);
      console.log('Registration data:', {
        email: data.email,
        username: data.username,
        password: '***',
        role: "USER"
      });

      const response = await axios.post(`${API_URL}/auth/register`, {
        email: data.email,
        username: data.username,
        password: data.password,
        role: "USER"
      });

      console.log('Registration response:', response.data);
    } catch (error) {
      console.error('Registration error details:', {
        isAxiosError: axios.isAxiosError(error),
        status: axios.isAxiosError(error) ? error.response?.status : 'N/A',
        statusText: axios.isAxiosError(error) ? error.response?.statusText : 'N/A',
        data: axios.isAxiosError(error) ? error.response?.data : 'N/A',
        message: error instanceof Error ? error.message : 'Unknown error'
      });

      if (axios.isAxiosError(error)) {
        const response = error.response?.data as ErrorResponse;
        if (response?.errors) {
          throw new Error(response.errors.join(', '));
        }
        throw new Error(response?.message || 'Registration failed. Please try again.');
      }
      throw error;
    }
  },

  logout() {
    localStorage.removeItem('token');
  },

  getToken(): string | null {
    return localStorage.getItem('token');
  },

  isAuthenticated() {
    return !!localStorage.getItem('token');
  }
};

// Add axios interceptor to add token to all requests
axios.interceptors.request.use(
  (config) => {
    const token = authService.getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
); 