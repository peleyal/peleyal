"""
Rules containing dependencies for different parts of this repository.
"""

load("@bazel_gazelle//:deps.bzl", "go_repository")

def go_repositories():
    """
    Defines required dependencies for the Go code in this repository.

    NOTE: This rule is automatically managed by Gazelle, using the go.mod file.
    Rebuild with:
    bazel run //:gazelle -- update-repos -from_file=go.mod -to_macro=repositories.bzl%go_repositories
    """
