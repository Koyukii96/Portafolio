package com.notas.websecurity.security;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;

@Configuration
public class SecurityConfig extends WebSecurityConfigurerAdapter {
	
	@Autowired
	CustomAuthenticationSuccessHandler customAuthenticationSuccessHandler;
	
    @Override
    protected void configure(HttpSecurity http) throws Exception {
        http
          .authorizeRequests()
          .antMatchers("/","/home").permitAll()
          .antMatchers("/admin").hasRole("ADMIN")
          .anyRequest().authenticated()
          .and()
          .formLogin().successHandler(customAuthenticationSuccessHandler)
          .loginPage("/login")
          .permitAll()
          .and()
          .logout()
          .permitAll();
        
        http.exceptionHandling().accessDeniedPage("/403");
    }
 
    @Autowired
    public void configureGlobal(AuthenticationManagerBuilder auth) throws Exception {
        auth
         .inMemoryAuthentication()
          .withUser("user").password("user").roles("USER")
 .and()
         .withUser("admin").password("admin").roles("ADMIN")
 .and()
         .withUser("koyuki").password("clave").roles("ADMIN");
    }
}