@echo off
    IF "%SITE_FLAVOR%" == "api" (
      deploy.api.cmd
    ) ELSE (
      IF "%SITE_FLAVOR%" == "web" (
        deploy.web.cmd
      ) ELSE (
        echo You have to set SITE_FLAVOR setting to either "api" or "web"
        exit /b 1
      )
    )